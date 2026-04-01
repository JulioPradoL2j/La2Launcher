using System;
using System.Collections.Generic;
using System.Management;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace La2Launcher
{
    public partial class Form1 : Form
    {
        public Dictionary<string, string> patchFiles = new Dictionary<string, string>()
        {
              { "1.0.0.0", "https://www.dropbox.com/scl/fi/5iq4vz20od620n2y7af95/system.zip?rlkey=zldgndplkivuxcu5h2d2r27g6&st=c7grxd0z&dl=1" },
              { "1.0.0.1", "https://www.dropbox.com/scl/fi/75r1ezq6n9652wgdao8h9/animation.zip?rlkey=hvmybt6xny6ghm6c34wdx28kz&st=qnxy9y6p&dl=1" }
        };

        // ===== CURSOR API =====
        [DllImport("user32.dll")]
        private static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        private struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        // ===== VARIABLES =====
        private Cursor defaultCursor;
        private Cursor actionCursor;
        private bool allowClose = false;

        public string gamePath = Application.StartupPath;
        private string LocalVersionFile => Path.Combine(gamePath, "version.dat");
        private readonly string forumUrl = "https://www.l2jbrasil.com";
        private System.Windows.Forms.Timer antiCheatTimer;
        private bool antiCheatAlertShown = false;
        private ManagementEventWatcher processStartWatcher;

        private readonly string[] blockedProcessNames =
        {
            "fileedit",
            "l2phx",
            "l2walker",
            "l2tower"
        };

        private readonly string[] blockedWindowTitles =
        {
            "fileedit",
            "l2phx",
            "l2walker",
            "l2tower"

        };

        private readonly string[] blockedDescriptions =
        {
            "fileedit",
            "l2phx",
            "l2walker",
            "l2tower"

        };



        // ===== Checks Apps.exe ChatEngine.exe L2Phx.exe
        private void InitAntiCheat()
        {
            antiCheatTimer = new System.Windows.Forms.Timer();
            antiCheatTimer.Interval = 3000;
            antiCheatTimer.Tick += (s, e) => ScanForbiddenProcesses();
            antiCheatTimer.Start();

            try
            {
                processStartWatcher = new ManagementEventWatcher(
                    new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
                processStartWatcher.EventArrived += (s, e) => ScanForbiddenProcesses();
                processStartWatcher.Start();
            }
            catch
            {
                // Se falhar o watcher, o timer continua cobrindo.
            }
        }
        private void ScanForbiddenProcesses()
        {
            try
            {
                foreach (Process proc in Process.GetProcesses())
                {
                    if (IsForbiddenProcess(proc))
                    {
                        HandleForbiddenProcess(proc);
                        return;
                    }
                }

                antiCheatAlertShown = false;
            }
            catch
            {
                // Evita quebrar o launcher por processo protegido do Windows.
            }
        }
        private bool IsForbiddenProcess(Process proc)
        {
            try
            {
                string processName = (proc.ProcessName ?? string.Empty).ToLowerInvariant();
                string windowTitle = (proc.MainWindowTitle ?? string.Empty).ToLowerInvariant();
                string description = GetFileDescriptionSafe(proc).ToLowerInvariant();

                if (ContainsAny(processName, blockedProcessNames))
                    return true;

                if (ContainsAny(windowTitle, blockedWindowTitles))
                    return true;

                if (ContainsAny(description, blockedDescriptions))
                    return true;
            }
            catch
            {
                // Alguns processos negam acesso
            }

            return false;
        }

        private bool ContainsAny(string source, string[] signatures)
        {
            if (string.IsNullOrWhiteSpace(source))
                return false;

            foreach (string sig in signatures)
            {
                if (source.Contains(sig))
                    return true;
            }

            return false;
        }
        private string GetFileDescriptionSafe(Process proc)
        {
            try
            {
                string path = proc.MainModule?.FileName;
                if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                    return string.Empty;

                FileVersionInfo info = FileVersionInfo.GetVersionInfo(path);
                return info.FileDescription ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        private void HandleForbiddenProcess(Process forbiddenProc)
        {
            try
            {
                //KillGameProcesses();

                // try
                // {
                //     forbiddenProc.Kill();
                // }
                // catch
                // {
                // Se não conseguir matar o hack, ao menos fecha o jogo.
                // }

                if (!antiCheatAlertShown)
                {
                    antiCheatAlertShown = true;
                }

               //     MessageBox.Show(
               //         "Aplicativo proibido detectado:\n\n" +
               //        $"Processo: {forbiddenProc.ProcessName}\n\n" +
               //         "O jogo foi fechado por segurança.",
               //        "Anti-Cheat",
               //         MessageBoxButtons.OK,
               //         MessageBoxIcon.Warning
               //     );
               // }

                SetStatus("Ferramenta proibida detectada.");
            }
            catch
            {
                SetStatus("Falha ao aplicar proteção.");
            }
        }

        private void KillGameProcesses()
        {
            foreach (Process proc in Process.GetProcesses())
            {
                try
                {
                    string name = (proc.ProcessName ?? string.Empty).ToLowerInvariant();

                    if (name == "l2" || name.Contains("lineage"))
                    {
                        proc.Kill();
                    }
                }
                catch
                {
                }
            }
        }

        // ===== CONSTRUCTOR =====
        public Form1()
        {

            InitializeComponent();
            InitTray();
            
        }

        // ===== LOAD =====
        private void Form1_Load(object sender, EventArgs e)
        {
            InitAntiCheat();

            // ===== CRIAR CURSOR PRIMEIRO =====
            defaultCursor = CreateCursor(Properties.Resources.cursor_default, 0, 0);
            actionCursor = CreateCursor(Properties.Resources.cursor_pointer, 5, 5);

            this.Cursor = defaultCursor;

            // ===== DEPOIS APLICAR HOVER =====
            ApplyHover(btnPlayArea);
            ApplyHover(btnFullCheckArea);
            ApplyHover(btnCancelArea);

            // Garantir ordem visual
            btnPlayArea.BringToFront();
            btnFullCheckArea.BringToFront();

            arquivosBar.BringToFront();
            lblStatus.BringToFront();
            btnCancelArea.BringToFront();

            // Timer
            var timer = new Timer { Interval = 1500 };
            timer.Tick += (s, ev) =>
            {
                timer.Stop();
                timer.Dispose();
                LauncherReady();
            };
            timer.Start();
        }


        private void RestartAsAdmin()
        {
            var exeName = Application.ExecutablePath;

            ProcessStartInfo startInfo = new ProcessStartInfo(exeName)
            {
                UseShellExecute = true,
                Verb = "runas" // 🔥 força admin
            };

            try
            {
                Process.Start(startInfo);
                Application.Exit();
            }
            catch
            {
                MessageBox.Show("Permissão de administrador recusada.");
            }
        }

        // ===== CORE =====
        private bool isUpdating = false;

        private async Task CheckUpdates()
        {
            if (isUpdating)
                return;

            isUpdating = true;

            try
            {
                string local = GetLocalVersion();

                foreach (var patch in patchFiles.Keys.OrderBy(v => new Version(v)))
                {
                    if (new Version(patch) > new Version(local))
                    {
                        SetStatus($"Aplicando patch {patch}...");

                        await DownloadAndExtract(patch);

                        SaveLocalVersion(patch);

                        local = patch;
                    }
                }

                SetStatus("Atualização concluída ✔");

                SetProgress(100);
            }
            catch (UnauthorizedAccessException)
            {
                var result = MessageBox.Show(
                    "O launcher precisa de permissão de administrador.\n\nDeseja reiniciar como admin?",
                    "Permissão necessária",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    RestartAsAdmin();
                }
                else
                {
                    SetStatus("Atualização cancelada.");

                    var timer = new Timer { Interval = 1500 };
                    timer.Tick += (s, ev) =>
                    {
                        timer.Stop();
                        timer.Dispose();
                        ExitLauncher();
                    };


                    timer.Start();
                }
            }
            catch (Exception ex)
            {
                SetStatus("Erro ao atualizar");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                isUpdating = false;
            }
        }

        private void SetStatus(string text)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action(() => lblStatus.Text = text));
            }
            else
            {
                lblStatus.Text = text;
            }
        }


        // ===== VERSION =====
        private string GetLocalVersion()
        {
            if (!File.Exists(LocalVersionFile))
                return "0.0.0.0";

            return File.ReadAllText(LocalVersionFile).Trim();
        }


        private void SaveLocalVersion(string version)
        {
            File.WriteAllText(LocalVersionFile, version);
        }

        private async Task DownloadSequential(string current, string[] versions)
        {
            bool start = string.IsNullOrEmpty(current);

            foreach (var v in versions)
            {
                string version = v.Trim();

                if (!start && version == current)
                {
                    start = true;
                    continue;
                }

                if (start)
                {
                    await DownloadAndExtract(version);
                }
            }
        }
        private async Task DownloadAndExtract(string version)
        {
            if (!patchFiles.ContainsKey(version))
                throw new Exception($"Patch não encontrado: {version}");

            string url = patchFiles[version];
            string zipPath = Path.Combine(gamePath, version + ".zip");

            SetStatus($"Baixando {version}...");
            arquivosBar.Value = 0;

            using (var client = new HttpClient())
            using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                var canReport = totalBytes != -1;

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var fs = new FileStream(zipPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    byte[] buffer = new byte[8192];
                    long totalRead = 0;
                    int read;

                    while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await fs.WriteAsync(buffer, 0, read);
                        totalRead += read;

                        if (canReport)
                        {
                            int percent = (int)((totalRead * 100) / totalBytes);

                            SetProgress(percent);
                            SetStatus($"Baixando {version} - {percent}%");
                        }
                    }
                }
            }

            SetStatus($"Extraindo {version}...");

            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (var entry in archive.Entries)
                {
                    string filePath = Path.Combine(gamePath, entry.FullName);

                    if (string.IsNullOrEmpty(entry.Name))
                    {
                        Directory.CreateDirectory(filePath);
                        continue;
                    }

                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    entry.ExtractToFile(filePath, true);
                }
            }

            File.Delete(zipPath);
        }
        private void SetProgress(int value)
        {
            if (arquivosBar.InvokeRequired)
            {
                arquivosBar.Invoke(new Action(() =>
                {
                    arquivosBar.Value = Math.Max(0, Math.Min(100, value));
                }));
            }
            else
            {
                arquivosBar.Value = Math.Max(0, Math.Min(100, value));
            }
        }
        // ===== CURSOR =====
        private Cursor CreateCursor(Bitmap bmp, int x, int y)
        {
            IntPtr ptr = bmp.GetHbitmap();

            IconInfo info = new IconInfo
            {
                fIcon = false,
                xHotspot = x,
                yHotspot = y,
                hbmMask = ptr,
                hbmColor = ptr
            };

            IntPtr iconPtr = CreateIconIndirect(ref info);
            DeleteObject(ptr);

            return new Cursor(iconPtr);
        }

        private void ApplyHover(Control ctrl)
        {
            ctrl.MouseEnter += (s, e) =>
            {
                this.Cursor = actionCursor;
                ctrl.Cursor = actionCursor;
            };

            ctrl.MouseLeave += (s, e) =>
            {
                this.Cursor = defaultCursor;
                ctrl.Cursor = defaultCursor;
            };
        }

        // ===== TRAY =====
        private void InitTray()
        {
            trayMenu.Items.Add("Open", null, (s, e) => ShowLauncher());
            trayMenu.Items.Add("Forum", null, (s, e) => OpenForum());
            trayMenu.Items.Add("Exit", null, (s, e) => ExitLauncher());

            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.DoubleClick += (s, e) => ShowLauncher();
        }

        private void ShowLauncher()
        {
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }

        private void ExitLauncher()
        {
            allowClose = true;
            trayIcon.Visible = false;
            Application.Exit();
        }

        // ===== ACTIONS =====
        private void LauncherReady()
        {
            _ = CheckUpdates();
            // Aqui entra patch / status futuramente
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            string exe = Path.Combine(Application.StartupPath, "system", "l2.exe");

            if (!File.Exists(exe))
            {
                MessageBox.Show("Cliente não encontrado.");
                return;
            }

            Process.Start(exe);
            SetStatus("Jogando!");
            Hide();
        }

        private void BtnCheckFull_Click(object sender, EventArgs e)
        {
            _ = CheckUpdates();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            bool gameRunning = Process.GetProcessesByName("l2").Length > 0;

            string message;

            if (gameRunning)
            {
                message =
                    "O jogo está em execução.\n\n" +
                    "Se você fechar o launcher, o jogo também será encerrado.\n\n" +
                    "Deseja continuar?";
            }
            else
            {
                message = "Deseja fechar o launcher?";
            }

            var result = MessageBox.Show(
                message,
                "L2Launcher",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                allowClose = true;

                // Se jogo estiver aberto → fecha também
                if (gameRunning)
                {
                    foreach (var p in Process.GetProcessesByName("l2"))
                    {
                      
                    }
                }

                Application.Exit();
            }
            else
            {
                Hide(); // minimiza estilo launcher oficial
            }
        }

        // ===== FORUM =====
        private void OpenForum()
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = forumUrl,
                UseShellExecute = true
            });
        }

        // ===== MINIMIZE =====
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (WindowState == FormWindowState.Minimized)
                Hide();
        }

        // ===== CLOSE CONTROL =====
        protected override void OnFormClosing(FormClosingEventArgs e)
        {

            if (!allowClose)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}