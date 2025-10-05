# Aicomi-Translation

Fan translations for Aicomi, for use with [XUnity Auto Translator](https://github.com/bbepis/XUnity.AutoTranslator).

You can see which languages are supported [here](https://github.com/IllusionMods/Aicomi-Translation/tree/main/Translation).

## Installation Instructions

1. Prerequisites
	- [BepInEx v6 IL2CPP](https://github.com/BepInEx/BepInEx) (latest nightly version)
	- [XUnity Auto Translator](https://github.com/bbepis/XUnity.AutoTranslator) (latest BepInEx6 IL2CPP version)

2. Delete the existing `BepInEx\Translation` folder from your game directory if it exists.

3. 安装此翻译
	- 从项目的[发布页面](https://github.com/IllusionMods/Aicomi-Translation/releases)下载最新的发布版本。
	- 将压缩包内的内容解压到您的游戏根目录中。如果系统询问，请选择覆盖文件。请确保 BepInEx\Translation 文件夹被正确放置。
	- 检查是否生效。

4. 检查是否生效
	- 如果您希望更改语言，请编辑 BepInEx\config\XUnity.AutoTranslator.cfg 文件，并将 Language 选项设置为您期望的语言代码（例如，Language=zh-CH）。
	- 启动游戏。翻译现在应该会自动应用。
	- 如果您看到未翻译的文本（例如，由模组或未来游戏更新添加的物品名称），您可能需要更改 Endpoint 选项。在游戏内按 Alt+0 打开 XUnity Auto Translator 菜单，并选择一个不同的翻译服务端点。


## Contributing

If you are comfortable with Git, fork the repository and submit PRs. Since the PRs will be squash merged, do not PR from your main branch or you will have issues if you try to PR again.

To add a new language, copy the `Translation/en` folder to a new folder with the appropriate [IETF language tag](https://en.wikipedia.org/wiki/IETF_language_tag) (e.g. `Translation/fr` for French), then only edit the files in that folder.

Otherwise, you can use the GitHub web interface to easily make single-file changes:

1. Browse the repository on GitHub and open the file you want to improve translations in.
2. Click the edit (pencil) icon at the top right of the file view to open the online editor.
3. Edit the text on the right of = as needed. Do not edit anything on the left of the = sign!
4. Loot for the "Propose changes" button. Add a short title and description for your change. Tell it to make a fork and a new branch, and start a pull request.

**Tip:** For small fixes (typos, single lines), editing on GitHub is easiest. For larger changes, you can still use the GitHub website by repeating the above steps for each file.

### 编译新发布版本

发布版本必须使用本代码库中包含的 TranslationTool 来制作，否则游戏中的一些 ADV 对话行将不会被翻译。这是因为该工具需要为包含代表游戏内角色名的占位符标签的文本行生成正则表达式。

首先在 Visual Studio 2022 或更高版本中构建整个翻译工具解决方案，然后通过将代码库文件夹拖放到 ReleaseTool 可执行文件上来运行它。该工具将在其可执行文件所在的同一文件夹内生成一个新的发布版本压缩包。
