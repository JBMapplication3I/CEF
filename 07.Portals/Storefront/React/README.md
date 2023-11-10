This project is created for and property of Clarity Ventures Inc.

Guidelines for developers:

1. Format your code (shortcut shift + alt + f or just save if using the workspace) before committing.
2. All components should be function-based (as opposed to class-based).
3. The name of a file should match the name of the component it contains.
4. Don't add anything into redux unless it's being used in several areas of the app (minimum 3) that are not parents/children of each other.
5. Don't install dependencies that are not well-established.
6. Currently the project is using function declarations (function foo() {}) for async functions and function expressions (const foo = () => {}) for components. This isn't a required pattern, but it's helpful to continue it for the sake of consistency (or change it everywhere).
7. Use only named exports and one export per file when dealing with React components. Feel free to have multiple exports per file if you're exporting a non-component function.
8. Use axios for api calls. You can find the documentation here: https://axios-http.com/docs/intro
9. Object destructuring is encouraged.
10. You must run the front end development server in an administrator terminal.
11. This app was built with Node 16.9.3. It seems to work with older versions, but it's something to be aware of when debugging.
12. You must have the Prettier - Code Formatter extension enabled and set as the default Workspace and User editor under Preferences > Settings
13. To develop on the 3000 port (hot reloading), you need to
    13.a Change the baseUrl in axios.ts to be an absolute one, e.g. https://clarity-local.clarityclient.com/DesktopModules/ClarityEcommerce/API-Storefront
    13.b Add the absolute url to the i18n.js file, e.g. axios.get("https://bid-local.clarityclient.com/DesktopModules/ClarityEcommerce/UI-Storefront" + "lib/cef/...")
    13.c Add the following tag to AngJS/web.config: <add name="Access-Control-Allow-Origin" value="https://clarity-local.clarityclient.com:3000" />

Installation Tips:

1. In package.json - scripts.build and scripts.start need to have the client skin at the end
2. For development, temporarily change the script imports in footer.ascx to the following. Dev server seems to use [id] as [name]
   <dnn:DnnJsInclude ID="DNNJSIncludeStoreVendors" runat="server" FilePath="https://react-dnn9.clarityclient.com:3000/DesktopModules/ClarityEcommerce/Shop/vendors-cef-store-vendors.js" />
   <dnn:DnnJsInclude ID="DNNJSIncludeStoreMain" runat="server" FilePath="https://react-dnn9.clarityclient.com:3000/DesktopModules/ClarityEcommerce/Shop/main-cef-store-main.js" />

Format All Files:

1. Install "Command on All Files" by rioj7
2. Extension Settings > Edit in settings.json
3. Make sure the settings below are added into your settings.json
4. To run it, press CTRL + SHIFT + P > Apply 1 or more commands to all files in the workspace > Format File

NOTE: Make sure you are in the workspace, and exclude files that are not supposed to be formatted.

```json
{
  "[javascript]": {
    "editor.defaultFormatter": "esbenp.prettier-vscode"
  },
  "prettier.tabWidth": 2,
  "prettier.trailingComma": "none",
  "prettier.useEditorConfig": false,
  "editor.tabSize": 2,
  "editor.detectIndentation": false,
  "editor.wordWrap": "on",
  "editor.defaultFormatter": "esbenp.prettier-vscode",
  "prettier.useTabs": true,
  "window.zoomLevel": 1,
  "editor.renderWhitespace": "all",
  "security.workspace.trust.untrustedFiles": "open",
  "diffEditor.ignoreTrimWhitespace": false,
  "editor.formatOnSave": true,
  "commandOnAllFiles.commands": {
    "Format File": {
      "command": "editor.action.formatDocument",
      "includeFileExtensions": [".tsx"]
    }
  },
  "commandOnAllFiles.includeFileExtensions": ["*.tsx"]
}
```

Happy coding!
