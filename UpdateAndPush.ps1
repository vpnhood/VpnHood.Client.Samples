# update
$solutionDir = $PSScriptRoot;
$gitDir = "$solutionDir/.git";
try
{
	Push-Location -Path "$solutionDir";
	
	# Update nugets
	dotnet-outdated -u;

	# build
	dotnet build

	# commit and push
	git --git-dir=$gitDir --work-tree=$solutionDir commit -a -m "Publish";
	git --git-dir=$gitDir --work-tree=$solutionDir pull;
	git --git-dir=$gitDir --work-tree=$solutionDir push;
}
finally
{
	Pop-Location
}

