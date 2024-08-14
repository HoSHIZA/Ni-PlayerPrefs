# NiPrefs: Improved Unity PlayerPrefs!

NiPrefs is a powerful and flexible wrapper for Unity's PlayerPrefs system, offering extended functionality and support for various data types. This library simplifies the process of storing and retrieving player preferences while providing a clean and extensible API.

## Features

- Easy-to-use wrapper for Unity's PlayerPrefs
- Support for primitive types (string, int, float, bool, Enum)
- Support for unity types (Color, Vector, Quaternion, Resolution)
- Extensible architecture for custom data types
- Type-safe operations with generics

## Requirements

* Unity 2019.3 or later

## Installation

### Manual

1. Clone this repository or download the source files.
2. Copy the `Ni-PlayerPrefs` folder into your Unity project's `Assets` directory.

### UPM

1. Open Package Manager from Window > Package Manager.
2. Click the "+" button > Add package from git URL.
3. Enter the following URL:

```
https://github.com/HoSHIZA/Ni-PlayerPrefs.git
```

## Usage

### Basic Usage

```csharp
using NiGames.PlayerPrefs;

// Set values
NiPrefs.Set("score", 100);
NiPrefs.Set("playerName", "John");
NiPrefs.Set("isNewPlayer", true);

// Get values (Preferred method)
int score = NiPrefs.GetInt("score");
string name = NiPrefs.GetString("playerName", "DefaultName");
bool isNew = NiPrefs.GetBool("isNewPlayer");

// Get values (Alternative method)
int score = NiPrefs.Get<int>("score");
string name = NiPrefs.Get<string>("playerName", "DefaultName");
bool isNew = NiPrefs.Get<bool>("isNewPlayer");

// Save changes
NiPrefs.Save();
```

### Working with Custom Types

NiPrefs allows you to extend its functionality to work with custom types by implementing the `IPlayerPrefsProvider<T>` interface.

```csharp
public readonly struct Vector3Provider : IPlayerPrefsProvider<Vector3>
{
    public Vector3 Get(string key, Vector3 defaultValue = default)
    {
        // Implementation
    }

    public void Set(string key, Vector3 value)
    {
        // Implementation
    }
}

// Register the custom provider
NiPrefs.RegisterProvider<Vector3, Vector3PlayerPrefsProvider>();

// Now you can use Vector3 with NiPrefs
Vector3 position = NiPrefs.Get<Vector3>("playerPosition", Vector3.zero);
NiPrefs.Set("playerPosition", new Vector3(1, 2, 3));
```

In addition, you can declare a partial class to simplify the invocation.

```csharp
namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetVector3(string key, Vector3 defaultValue = default) 
            => default(Vector3PlayerPrefsProvider).Get(key, defaultValue);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(string key, Vector3 value) 
            => default(Vector3PlayerPrefsProvider).Set(key, value);
    }
}
```

## License

This project is licensed under the [MIT License](LICENSE).