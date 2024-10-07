# Blackjax - Blackjack Game

A Windows Forms-based Blackjack game built with C# and .NET Framework 4.7.2. Features a complete casino-style blackjack experience with multimedia elements, card graphics, and database integration.

## Features

- 🃏 Complete deck of playing cards with high-quality graphics
- 🎮 Interactive gameplay with dealer and player hands
- 🎵 Audio and video elements for enhanced gaming experience
- 💾 Database integration for game data storage
- 🖥️ Windows Forms GUI with multiple game screens
- 📊 Game statistics and data tracking

## System Requirements

- **Operating System**: Windows 10/11
- **Framework**: .NET Framework 4.7.2 or higher
- **Resolution**: **1360 × 768 pixels**
  
  ⚠️ **Important**: This game is designed specifically for 1360×768 resolution and may not display correctly on other screen resolutions.

## Prerequisites

Before running the game, ensure you have:

1. **Windows Operating System** (Windows 10 or later recommended)
2. **.NET Framework 4.7.2** or higher installed
3. **Visual Studio** (Community, Professional, or Enterprise) OR **VS Code with C# Dev Kit**

## How to Run

### Method 1: Quick Launch (Pre-compiled)

The easiest way to run the game if it's already compiled:

1. Navigate to the project directory
2. Open PowerShell or Command Prompt
3. Run the following commands:
   ```powershell
   cd "bin\Debug"
   .\blackjax.exe
   ```

### Method 2: Using Visual Studio

1. Open `blackjax.sln` in Visual Studio
2. Build the solution (Build → Build Solution or press `F6`)
3. Run the application (Debug → Start Debugging or press `F5`)

### Method 3: Using VS Code

1. Install the **C# Dev Kit** extension in VS Code
2. Open the project folder in VS Code
3. Use the terminal in VS Code:
   ```powershell
   cd "bin\Debug"
   .\blackjax.exe
   ```

Or use the build tasks:
- Press `Ctrl+Shift+P` → "Tasks: Run Build Task" to build
- Press `F5` to run/debug

### Method 4: Command Line Build

If you have MSBuild in your PATH:
```powershell
msbuild blackjax.csproj
cd "bin\Debug"
.\blackjax.exe
```

If MSBuild is not in PATH, use the full path:
```powershell
"C:\Program Files\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe" blackjax.csproj
cd "bin\Debug"
.\blackjax.exe
```

## Project Structure

```
blackjax/
├── Form1.cs - Form5.cs          # Game UI forms
├── blackjack.cs                 # Main game logic
├── GameData.cs                  # Game data management
├── Program.cs                   # Application entry point
├── Database1.mdf                # Game database
├── bin/Debug/inventory/         # Game assets
│   ├── images/                  # Card images
│   ├── *.mp4                    # Video files
│   └── *.png                    # Game graphics
└── Properties/                  # Assembly information
```

## Game Assets

The game includes:
- **52 card images** (10C.jpg, 2S.jpg, etc.) in `bin/Debug/inventory/images/`
- **Video files** for tutorials and gameplay
- **Audio files** and dealer graphics
- **Database files** for storing game data

## Troubleshooting

### Common Issues:

1. **Game doesn't display correctly**
   - Ensure your screen resolution is set to 768×866 or similar aspect ratio
   - Check Windows display scaling settings

2. **"MSBuild not found" error**
   - Install Visual Studio Build Tools or Visual Studio Community Edition
   - Or use the pre-compiled version in `bin/Debug/`

3. **Missing .NET Framework**
   - Download and install .NET Framework 4.7.2 from Microsoft's website

4. **Database errors**
   - Ensure `Database1.mdf` and `Database1_log.ldf` are in the correct directories
   - Check that SQL Server LocalDB is installed

## Development

To modify or extend the game:

1. Clone the repository
2. Open `blackjax.sln` in Visual Studio
3. Make your changes
4. Build and test
5. The game uses Windows Forms, so UI changes should be made through the Visual Studio designer

## Contributing

Feel free to fork this repository and submit pull requests for improvements!

## some preview 
<img width="1920" height="1080" alt="Screenshot (7)" src="https://github.com/user-attachments/assets/bcaa1c26-51aa-4817-8f91-a51349279965" />
<img width="1920" height="1080" alt="Screenshot (11)" src="https://github.com/user-attachments/assets/ebf06427-8a4e-4bc2-a2db-eb6efc56e94e" />
<img width="1920" height="1080" alt="Screenshot (10)" src="https://github.com/user-attachments/assets/fe94cbb5-6a5c-4019-8fae-7bbb18017167" />
<img width="1366" height="768" alt="Screenshot (12)" src="https://github.com/user-attachments/assets/14dce7c6-a663-43c6-b69b-7ea37325265b" />



---

**Note**: This game was designed for a specific screen resolution (768×866). For the best experience, adjust your display settings accordingly before running the game.
