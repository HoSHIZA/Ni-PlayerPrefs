# Ni-Prefs: Improved Unity PlayerPrefs!

Ni-Prefs is a powerful and flexible wrapper for Unity's PlayerPrefs system, offering extended functionality and support for various data types. This library simplifies the process of storing and retrieving player preferences while providing a clean and extensible API.

## Features

- Easy-to-use wrapper for Unity's PlayerPrefs.
- Support for primitive types (`string`, `int`, `float`, `bool`, `Enum`).
- Support for unity types (`Color`, `Vector`, `Quaternion`, `Resolution`).
- Encryption of `keys` and/or `values`.
- Extensible architecture for custom data types.

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

### Manual with `manifest.json`

1. Open `manifest.json`.
2. Add the following line to the file:

```
"com.ni-games.ni_prefs" : "https://github.com/HoSHIZA/Ni-PlayerPrefs.git"
```

## Setting

It is possible to configure logging and enable encryption.

```csharp
using NiGames.PlayerPrefs;

// Enable logging.
NiPrefs.Settings.EnableLogging = true;

// Enable key encryption using Unique Device Key.
NiPrefs.Settings.EncryptKey = true;

// Enable value encryption using Unique Device Key.
NiPrefs.Settings.EncryptValue = true;

// Sets a new hash that is used for encryption.
// When null or empty, `Unique Device Key` is used.
NiPrefs.Settings.EncryptionHash = "hash";
```

## Usage

```csharp
using NiGames.PlayerPrefs;

// Set values
NiPrefs.Set("score", 100);
NiPrefs.Set("playerName", "John");
NiPrefs.Set("isNewPlayer", true);
NiPrefs.SetEnum("playerState", PlayerState.Idle); // Enum must be used explicitly.

// Get values using direct method (Preferred method).
int score = NiPrefs.GetInt("score");
string name = NiPrefs.GetString("playerName", "DefaultName");
bool isNew = NiPrefs.GetBool("isNewPlayer");

// Get values using provider resolving.
int score = NiPrefs.Get<int>("score");
string name = NiPrefs.Get<string>("playerName", "DefaultName");
bool isNew = NiPrefs.Get<bool>("isNewPlayer");

// Enum must be used explicitly. Doesn't work with Get<T>/Set<T>.
PlayerState state = NiPrefs.GetEnum("playerState", PlayerState.DefaultState);

// Save changes
NiPrefs.Save();
```

It is also possible to store objects in Json and Binary representation.

```csharp
using NiGames.PlayerPrefs;

// Set value
NiPrefs.SetJson<Player>("playerJson", new Player());
NiPrefs.SetBinary<Player>("playerBinary", new Player());

// Get Value
Player playerJson = NiPrefs.GetJson<Player>("playerJson", null);
Player playerBinary = NiPrefs.GetBinary<Player>("playerBinary", null);
```

In some scenarios, you need to check the type of PlayerPrefs entry, this can be done using methods for type checking.

```csharp
// Returns an Enum `PlayerPrefsType` indicating the type of entry.
// If there is an error while getting the value, `PlayerPrefsType.Invalid` is returned.
NiPrefs.ParseEntryType("key");

var isString = NiPrefs.IsString("key"); // True if entry is string.
var isFloat = NiPrefs.IsFloat("key"); // True if entry is float.
var isInt = NiPrefs.IsInt("key"); // True if entry is int.
```

### Encryption

The default settings from Settings are used for encryption, 
but this can be overridden for each new Get/Set by passing the desired 
`PlayerPrefsEncryption encryption` as an argument to the methods.

### Working with Custom Types

NiPrefs allows you to extend its functionality to work with custom types by implementing the `IPlayerPrefsProvider<T>` interface.

```csharp
using NiGames.PlayerPrefs;

internal readonly struct Vector3Provider : IPlayerPrefsProvider<Vector3>
{
    public Vector3 Get(string key, Vector3 defaultValue = default, PlayerPrefsEncryption encryption = default)
    {
        // Implementation
        
        // Use `NiPrefs.Internal.Get instead` `of UnityEditor.PlayerPrefs.Get` to support encryption.
    }

    public void Set(string key, Vector3 value, PlayerPrefsEncryption encryption = default)
    {
        // Implementation
        
        // Use `NiPrefs.Internal.Set` instead of `UnityEditor.PlayerPrefs.Set` to support encryption.
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

You can now use this shorthand for read or write a custom type value from PlayerPrefs.
> It is not necessary to register the provider with this entry, but it must be registered when using generic Get<T>/Set<T>.
```csharp
NiPrefs.GetVector3("playerPosition", Vector3.zero);
```

## TODO

* Support for various encryption methods.
* Combining `Get<T>`/`Set<T>` and `GetEnum`/`SetEnum`.

## License

This project is licensed under the [MIT License](LICENSE).