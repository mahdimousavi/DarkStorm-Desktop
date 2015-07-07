[T4Scaffolding.Scaffolder(Description = "Creates an IRepository")][CmdletBinding()]
param(        
    [parameter(Position = 0, Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$ModelType,
    [string]$Project,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)


$foundModelType = Get-ProjectType $ModelType -Project $Project
if (!$foundModelType) { return }
		
$outputPath = Join-Path IRepositories ("I"+$foundModelType.Name + "Repository")

Add-ProjectItemViaTemplate $outputPath -Template DarkStorm.IRepository -Model @{
	ModelType = [MarshalByRefObject]$foundModelType;
} -SuccessMessage "Added IRepository '{0}'" -TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
