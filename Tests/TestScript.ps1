param (
    [string]$port = "",  # COM port for hardware tests (optional)
    [int]$baudRate = 0   # Baud rate for hardware tests (optional)
)

# Шлях до файлів з тестами
$softwareTestPath = "D:\Study\4.1_course\AKPS\Labs\lab3\SoftwareTests.ps1"
$hardwareTestPath = "D:\Study\4.1_course\AKPS\Labs\lab3\HardwareTests.ps1"
$createDocsPath = "D:\Study\4.1_course\AKPS\Labs\lab3\CreateDocs.ps1"  # Шлях до CreateDocs.ps1

Write-Host "TestScript.ps1 script started."
Write-Host "Input parameters: port = '$port', baudRate = '$baudRate'."

if ($port -ne "" -and $baudRate -ne 0) {
    Write-Host "Running software and hardware tests..."
    & $softwareTestPath
    & $hardwareTestPath -port $port -baudRate $baudRate
} else {
    Write-Host "Running software tests only..."
    & $softwareTestPath
}

# Виконуємо скрипт CreateDocs.ps1 в кінці
Write-Host "Running CreateDocs.ps1..."
& $createDocsPath

Write-Host "TestScript.ps1 script finished."
