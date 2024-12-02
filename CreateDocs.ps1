# PowerShell Script to Install Doxygen and Generate Documentation

# Define paths
$doxygenInstallerUrl = "https://doxygen.nl/files/doxygen-1.12.0-setup.exe" # URL for Doxygen installer
$doxygenInstallerPath = "$env:TEMP\doxygen-setup.exe"
$projectDirs = "D:/Study/4.1_course/AKPS/Labs/lab3/APKS_Lab3 D:/Study/4.1_course/AKPS/Labs/lab3/ESP8266_CH340/ESP8266_CH340_3"
$outputDir = "D:\Study\4.1_course\AKPS\Labs\lab3\docs"

# Step 1: Check if Doxygen is installed
Write-Output "Checking if Doxygen is installed..."
$doxygenPath = (Get-Command "doxygen" -ErrorAction SilentlyContinue).Source

if (-not $doxygenPath) {
    Write-Output "Doxygen not found. Downloading and installing Doxygen..."

    # Download Doxygen installer
    Invoke-WebRequest -Uri $doxygenInstallerUrl -OutFile $doxygenInstallerPath -UseBasicParsing

    # Run the installer silently
    Start-Process -FilePath $doxygenInstallerPath -ArgumentList "/S" -Wait

    # Confirm installation
    $doxygenPath = (Get-Command "doxygen" -ErrorAction SilentlyContinue).Source
    if (-not $doxygenPath) {
        Write-Output "Doxygen installation failed. Please install it manually."
        exit 1
    }
    # Add Doxygen to PATH (if needed)
    $doxygenBinPath = "C:\Program Files\doxygen\bin"
    if (-not ($env:Path -split ';' | ForEach-Object { $_ -eq $doxygenBinPath })) {
        [System.Environment]::SetEnvironmentVariable("Path", $env:Path + ";$doxygenBinPath", [System.EnvironmentVariableTarget]::Machine)
        Write-Output "Doxygen path added to system PATH."
    }

    Write-Output "Doxygen installed successfully."
} else {
    Write-Output "Doxygen is already installed at $doxygenPath."
}

# Step 2: Create Doxygen configuration file if not exists
$doxyfilePath = "D:\Study\4.1_course\AKPS\Labs\lab3\Doxyfile"
if (-not (Test-Path $doxyfilePath)) {
    Write-Output "Generating Doxygen configuration file..."
    Start-Process -FilePath "doxygen" -ArgumentList "-g $doxyfilePath" -Wait
}

# Step 3: Update configuration file for your project settings
(Get-Content $doxyfilePath) -replace 'OUTPUT_DIRECTORY.*', "OUTPUT_DIRECTORY = $outputDir" | Set-Content $doxyfilePath
(Get-Content $doxyfilePath) -replace 'INPUT.*', "INPUT = $projectDirs" | Set-Content $doxyfilePath
(Get-Content $doxyfilePath) -replace 'RECURSIVE.*', "RECURSIVE = YES" | Set-Content $doxyfilePath
(Get-Content $doxyfilePath) -replace 'EXTRACT_ALL.*', "EXTRACT_ALL = YES" | Set-Content $doxyfilePath

# Step 4: Run Doxygen to generate documentation
Write-Output "Generating documentation..."
Start-Process -FilePath "doxygen" -ArgumentList "$doxyfilePath" -Wait

Write-Output "Documentation generation complete. Output available at $outputDir."
