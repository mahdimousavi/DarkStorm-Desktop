[T4Scaffolding.Scaffolder(Description = "Creates an Repository")][CmdletBinding()]
param(        
    [parameter(Position = 0, Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$ModelType,
    [string]$Project,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)


$foundModelType = Get-ProjectType $ModelType -Project $Project
if (!$foundModelType) { return }
		
$outputPath = Join-Path Repositories ($foundModelType.Name + "Repository")

Add-ProjectItemViaTemplate $outputPath -Template DarkStorm.Repository -Model @{
	ModelType = [MarshalByRefObject]$foundModelType;
} -SuccessMessage "Added Repository '{0}'" -TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
