param (
    [string]$testFile,  # Name of the test file
    [string]$port = "",  # COM port for hardware tests (optional)
    [int]$baudRate = 0   # Baud rate for hardware tests (optional)
)

# Path to files with tests
$softwareTestPath = "D:\Study\4.1_course\AKPS\Labs\lab3\SoftwareTests.ps1"
$hardwareTestPath = "D:\Study\4.1_course\AKPS\Labs\lab3\HardwareTests.ps1"

Write-Host "TestScript.ps1 script started."
Write-Host "Input parameters: testFile = '$testFile', port = '$port', baudRate = '$baudRate'."

# Check if the test file is specified
if ($testFile -eq "SoftwareTests.ps1") {
    Write-Host "Running software tests..."
    & $softwareTestPath
} else {
    Write-Host "Test file not found or not specified: $testFile."
}

# If port and baud rate are provided, run hardware tests
if ($port -ne "" -and $baudRate -ne 0) {
    Write-Host "Running hardware tests on port $port with baud rate $baudRate..."
    & $hardwareTestPath -port $port -baudRate $baudRate
} else {
    Write-Host "Hardware tests are not executed. Port and baud rate parameters are missing."
}
