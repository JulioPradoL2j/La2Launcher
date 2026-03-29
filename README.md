# 🎮 L2Launcher - Interlude Patch System & Anti-Cheat

Launcher profissional desenvolvido para servidores **Lineage II Interlude (L2J)** com sistema de atualização automática e proteção básica contra ferramentas externas.

---

## 🚀 Funcionalidades

### 🔄 Sistema de Patch Automático

* Atualização incremental baseada em versão
* Download direto via **Dropbox (link direto)**
* Extração automática de arquivos
* Barra de progresso em tempo real
* Controle de versão (`version.dat`)

---

### 🛡️ Anti-Cheat (Client-Side)

Sistema de proteção integrado que monitora processos suspeitos:

* Cheat Engine
* L2Phx
* L2Walker
* FileEdit / L2 FileEdit
* CPreload
* Injectors e ferramentas similares

#### 🔍 Métodos de detecção:

* Nome do processo
* Título da janela
* Descrição do executável

#### ⚡ Ações automáticas:

* Fecha o jogo (`l2.exe`)
* Tenta encerrar o aplicativo suspeito
* Notifica o jogador

---

### 🖥️ Interface

* UI personalizada estilo MMORPG
* Botões invisíveis sobre imagem (Play / Full Check / Cancel)
* Cursores customizados
* Sistema de tray (minimizar para bandeja)

---

### 🔐 Execução como Administrador

* Manifest configurado para `requireAdministrator`
* Evita erros de permissão (especialmente em `system/`)

---

## 📂 Estrutura do Patch

Os patches são definidos via dicionário:

```csharp
public Dictionary<string, string> patchFiles = new Dictionary<string, string>()
{
    { "1.0.0.0", "https://www.dropbox.com/...&dl=1" }
};
```

✔ Cada versão corresponde a um `.zip`
✔ O conteúdo é extraído diretamente no diretório do cliente

---

## 📥 Fluxo de Atualização

1. Launcher inicia
2. Lê versão local (`version.dat`)
3. Compara com patches disponíveis
4. Baixa versões faltantes
5. Extrai arquivos automaticamente
6. Atualiza versão local

---

## ⚠️ Requisitos

* .NET Framework 4.8
* Windows 10/11
* Permissão de administrador

---

## 📌 Recomendação

❌ NÃO instalar em:

```
C:\Program Files
```

✔ Use:

```
C:\Games\Lineage2
```

---

## 🧠 Limitações do Anti-Cheat

Este sistema é **client-side**, ou seja:

* Não substitui validações no servidor
* Pode ser burlado por usuários avançados
* Serve como proteção básica contra uso comum

---

## 👨‍💻 Autor

Projeto desenvolvido por **BAN-L2JDEV**

---

## 🌐 Comunidade

🔗 https://www.l2jbrasil.com

---

## ⭐ Contribuição

Sinta-se livre para contribuir com melhorias, sugestões ou correções.

---

## ⚖️ Licença

Uso livre para servidores privados de Lineage II.

---
