[T4Scaffolding.Scaffolder(Description = "Creates an IService")][CmdletBinding()]
param(        
    [parameter(Position = 0, Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$ModelType,
    [string]$Project,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)


$foundModelType = Get-ProjectType $ModelType -Project $Project
if (!$foundModelType) { return }
		
$outputPath = Join-Path IServices ("I"+$foundModelType.Name + "Service")

Add-ProjectItemViaTemplate $outputPath -Template DarkStorm.IService -Model @{
	ModelType = [MarshalByRefObject]$foundModelType;
} -SuccessMessage "Added IService '{0}'" -TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
