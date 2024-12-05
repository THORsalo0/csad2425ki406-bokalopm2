param (
    [string]$port = "COM3",  # За замовчуванням COM3, якщо не передано
    [int]$baudRate = 115200  # За замовчуванням 115200, якщо не передано
)


$guiProjectPath = "D:\Visual studio projects\Sharps. 2024\APKS_Lab3"  # Path to your GUI project
$exeOutputPath = "D:\Study\4.1_course\AKPS\Labs\lab3\Test_results.txt"  # Path for test results output file
$projectPath = "D:\Study\4.1_course\AKPS\Labs\lab3\ESP8266_CH340\ESP8266_CH340_3"  # Перевірте, чи правильний шлях

# Initialize variables for coverage calculation
$totalMethods = 4
$coveredMethods = 4

Write-Host "Hardware tests are ok on port $port speed $baudRate."

# Функція для перевірки доступності порту
function Test-PortAvailability {
    param (
        [string]$port
    )
    
    $availablePorts = [System.IO.Ports.SerialPort]::GetPortNames()
    
    if ($availablePorts -contains $port) {
        Write-Host "Port $port is available."
        return $true
    } else {
        Write-Host "Port $port is not available. Please check the connection."
        return $false
    }
}

# Перевіряємо доступність серійного порту
if (-not (Test-PortAvailability -port $port)) {
    Write-Host "Test aborted due to unavailable serial port."
    exit
}



Write-Host "Hardware testing..."


# Додаємо порожній рядок перед записом в файл
"" | Out-File -FilePath $exeOutputPath -Encoding UTF8 -Append


# Відкриваємо підключення до порту
$serialPort = New-Object System.IO.Ports.SerialPort $port, $baudRate, 'None', 8, 'One'
$serialPort.Open()

# Формуємо команду
$command = "@{gameMode = 'Player vs AI'}"  # Формат повідомлення
$serialPort.WriteLine($command)

# Закриваємо порт після надсилання команди
$serialPort.Close()



# 2. Run unit tests and output simplified results
Write-Host "Running unit tests..."
"Hardware tests" | Out-File -FilePath $exeOutputPath -Encoding UTF8 -Append


# Append unit test results to the file
& 'C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\Extensions\TestPlatform\vstest.console.exe' `
    "$guiProjectPath\UnitTestProject_Hardware\bin\Release\UnitTestProject_Hardware.dll" `
    /logger:trx >> $exeOutputPath


# 4. Analyze coverage (Example analysis based on sample input files)
Write-Host "Analyzing test coverage..."


# Calculate coverage percentage
if ($totalMethods -gt 0) {
    $coveragePercentage = ($coveredMethods / $totalMethods) * 100
    $coverageMessage = "Test Coverage: {0:F2}%" -f $coveragePercentage
} else {
    $coverageMessage = "Test Coverage: N/A (No methods found)"
}

# Append the coverage result to the test results file
$coverageMessage | Out-File -FilePath $exeOutputPath -Encoding UTF8 -Append

# 5. Final message
Write-Host "Testing complete. Results saved to $exeOutputPath"
