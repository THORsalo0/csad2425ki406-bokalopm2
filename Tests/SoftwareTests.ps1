# Define paths for the project
$projectPath = "D:\Study\4.1_course\AKPS\Labs\lab3\ESP8266_CH340\ESP8266_CH340_3"
$guiProjectPath = "D:\Visual studio projects\Sharps. 2024\APKS_Lab3"  # Path to your GUI project
$exeOutputPath = "D:\Study\4.1_course\AKPS\Labs\lab3\Test_results.txt"  # Path for test results output file

# 1. Build GUI project
Write-Host "Building GUI application..."
cd $guiProjectPath
& "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" "APKS_Lab3.sln" /p:Configuration=Release

# 2. Run GUI tests
Write-Host "Running GUI tests..."
Start-Process -FilePath "$guiProjectPath\APKS_Lab3\bin\Release\APKS_Lab3.exe" -Wait

# 3. Run unit tests and output simplified results
Write-Host "Running unit tests..."
# Add header to the test results file
"Software tests" | Out-File -FilePath $exeOutputPath -Encoding UTF8

# Append unit test results to the file
& 'C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\Extensions\TestPlatform\vstest.console.exe' `
    "$guiProjectPath\UnitTestProject_Lab3_Software\bin\Release\UnitTestProject_Lab3_Software.dll" `
    /logger:console >> $exeOutputPath

# 4. Final message
Write-Host "Testing complete. Results saved to $exeOutputPath"
