# 🎮 L2Launcher — Professional Patch & Anti-Cheat System (Interlude)

Launcher profissional para servidores **Lineage II Interlude (L2J)**, com sistema de atualização automática, proteção client-side e interface personalizada estilo MMORPG.

---

## ✨ Destaques

* 🔄 Patch automático incremental
* ⚡ Download direto via Dropbox (sem HTML / sem bloqueios)
* 🛡️ Anti-cheat integrado (process + window + file scan)
* 🎯 UI customizada (botões invisíveis + imagem)
* 🔐 Execução obrigatória como Administrador
* 📊 Barra de progresso real (download + status)
* 🧠 Sistema de versão (`version.dat`)

---

## 🖼️ Preview

> Interface estilo launcher oficial (MMORPG)
> Botões: **Play / Full Check / Cancel**

---

## ⚙️ Tecnologias

* .NET Framework 4.8
* Windows Forms (WinForms)
* C#
* HttpClient (download streaming)
* System.Management (anti-cheat watcher)

---

## 📦 Estrutura do Projeto

```text
La2Launcher/
 ├── Form1.cs
 ├── Form1.Designer.cs
 ├── Resources/
 ├── app.manifest
 ├── La2Launcher.csproj
```

---

## 🔄 Sistema de Patch

Os patches são controlados via código:

```csharp
public Dictionary<string, string> patchFiles = new Dictionary<string, string>()
{
    { "1.0.0.0", "https://www.dropbox.com/...&dl=1" }
};
```

### 🔥 Como funciona

1. Lê `version.dat`
2. Compara versões
3. Baixa `.zip` do Dropbox
4. Extrai automaticamente
5. Atualiza versão local

---

## 🛡️ Anti-Cheat (Client-Side)

Detecção baseada em:

* Nome do processo
* Título da janela
* Descrição do executável

### 🔍 Exemplos bloqueados

* Cheat Engine
* L2Phx
* L2Walker
* FileEdit
* CPreload
* Injectors

### ⚡ Ação automática

* Fecha o jogo (`l2.exe`)
* Tenta encerrar o hack
* Notifica o jogador

---

## ⚠️ Limitações

Este anti-cheat é **client-side**:

* Não impede usuários avançados
* Deve ser combinado com validações no servidor (Java)

---

# 🧪 COMO COMPILAR (TUTORIAL COMPLETO)

## 📥 1. Requisitos

Instale:

* Visual Studio (2019 ou superior)
* .NET Framework 4.8 SDK

Durante instalação do Visual Studio, marque:

✔ Desktop development with .NET

---

## 📂 2. Clonar ou baixar o projeto

Via Git:

```bash
git clone https://github.com/SEU_USUARIO/La2Launcher.git
```

Ou baixe o ZIP direto do GitHub.

---

## 🧩 3. Abrir no Visual Studio

1. Abra o Visual Studio
2. Clique em:

```text
Open a project or solution
```

3. Selecione:

```text
La2Launcher.sln
```

---

## ⚙️ 4. Verificar configuração

Abra:

```text
Project → Properties
```

Confirme:

* Target Framework: `.NET Framework 4.8`
* Output Type: `Windows Application`

---

## 🔐 5. Manifest (ADMIN)

Certifique-se que o projeto possui:

```xml
<requestedExecutionLevel level="requireAdministrator" uiAccess="false" />
```

E que está selecionado em:

```text
Application → Manifest
```

---

## 🔧 6. Restaurar dependências

Se necessário:

```text
Build → Restore NuGet Packages
```

---

## ▶️ 7. Compilar

```text
Build → Build Solution
```

Ou pressione:

```text
Ctrl + Shift + B
```

---

## 🚀 8. Executar

```text
F5
```

ou:

```text
Start Debugging
```

---

## 📦 9. Gerar EXE final

Após build:

```text
/bin/Debug/
ou
/bin/Release/
```

Arquivo:

```text
La2Launcher.exe
```

---

# 📁 Estrutura do Cliente

```text
Lineage2/
 ├── system/
 ├── La2Launcher.exe
 ├── version.dat
```

---

# ⚠️ IMPORTANTE

## ❌ NÃO usar:

```text
C:\Program Files
```

## ✅ Use:

```text
C:\Games\Lineage2
```

---

# 🧱 Installer (Recomendado)

Use:

* Inno Setup

Para criar instalador profissional.

---

# 🧠 Arquitetura

### Launcher

* UI + Patch + Anti-cheat

### Servidor (Recomendado)

* Validação de packets
* Anti-speed / anti-abuse
* Controle de sessão

---

# 🔥 Roadmap

* [ ] MD5 check dos arquivos
* [ ] Retry automático
* [ ] CDN próprio
* [ ] Anti-cheat avançado (memória)
* [ ] Heartbeat launcher → servidor
* [ ] Proteção HWID

---

# 👨‍💻 Autor

**BAN-L2JDEV**

---

# 🌐 Comunidade

🔗 https://www.l2jbrasil.com

---

# ⭐ Contribua

Pull Requests são bem-vindos.

---

# ⚖️ Licença

Uso livre para servidores privados de Lineage II.

---
