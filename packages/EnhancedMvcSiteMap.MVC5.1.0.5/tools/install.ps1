# install.ps1
param($rootPath, $toolsPath, $package, $project)

$DTE.ItemOperations.Navigate("http://matts-lno.github.io/MvcSiteMapProvider/", $DTE.vsNavigateOptions.vsNavigateOptionsNewWindow)
