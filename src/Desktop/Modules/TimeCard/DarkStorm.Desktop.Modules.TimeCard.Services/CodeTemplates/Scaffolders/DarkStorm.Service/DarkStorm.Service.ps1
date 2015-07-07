[T4Scaffolding.Scaffolder(Description = "Creates a Service")][CmdletBinding()]
param(        
    [parameter(Position = 0, Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$ModelType,
    [string]$Project,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)


$foundModelType = Get-ProjectType $ModelType -Project $Project
if (!$foundModelType) { return }
		
$outputPath =  ($foundModelType.Name + "Service")

Add-ProjectItemViaTemplate $outputPath -Template DarkStorm.Service -Model @{
	ModelType = [MarshalByRefObject]$foundModelType;
} -SuccessMessage "Added Service '{0}'" -TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
