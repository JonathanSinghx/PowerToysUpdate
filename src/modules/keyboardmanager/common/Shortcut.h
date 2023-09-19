#pragma once
#include "ModifierKey.h"

#include <compare>
#include <tuple>
#include <variant>

namespace KeyboardManagerInput
{
    class InputInterface;
}
class LayoutMap;

class Shortcut
{
private:
    // Function to split a wstring based on a delimiter and return a vector of split strings
    std::vector<std::wstring> splitwstring(const std::wstring& input, wchar_t delimiter);

    inline auto comparator() const
    {
        return std::make_tuple(winKey, ctrlKey, altKey, shiftKey, actionKey);
    }

public:
    ModifierKey winKey = ModifierKey::Disabled;
    ModifierKey ctrlKey = ModifierKey::Disabled;
    ModifierKey altKey = ModifierKey::Disabled;
    ModifierKey shiftKey = ModifierKey::Disabled;
    DWORD actionKey = {};

    Shortcut() = default;

    // Constructor to initialize Shortcut from it's virtual key code string representation.
    Shortcut(const std::wstring& shortcutVK);

    // Constructor to initialize shortcut from a list of keys
    Shortcut(const std::vector<int32_t>& keys);

    inline friend auto operator<=>(const Shortcut& lhs, const Shortcut& rhs) noexcept
    {
        return lhs.comparator() <=> rhs.comparator();
    }

    inline friend bool operator==(const Shortcut& lhs, const Shortcut& rhs) noexcept
    {
        return lhs.comparator() == rhs.comparator();
    }

    // Function to return the number of keys in the shortcut
    int Size() const;

    // Function to return true if the shortcut has no keys set
    bool IsEmpty() const;

    // Function to reset all the keys in the shortcut
    void Reset();

    // Function to return the action key
    DWORD GetActionKey() const;

    // Function to return the virtual key code of the win key state expected in the shortcut. Argument is used to decide which win key to return in case of both. If the current shortcut doesn't use both win keys then arg is ignored. Return NULL if it is not a part of the shortcut
    DWORD GetWinKey(const ModifierKey& input) const;

    // Function to return the virtual key code of the ctrl key state expected in the shortcut. Return NULL if it is not a part of the shortcut
    DWORD GetCtrlKey() const;

    // Function to return the virtual key code of the alt key state expected in the shortcut. Return NULL if it is not a part of the shortcut
    DWORD GetAltKey() const;

    // Function to return the virtual key code of the shift key state expected in the shortcut. Return NULL if it is not a part of the shortcut
    DWORD GetShiftKey() const;

    // Function to check if the input key matches the win key expected in the shortcut
    bool CheckWinKey(const DWORD input) const;

    // Function to check if the input key matches the ctrl key expected in the shortcut
    bool CheckCtrlKey(const DWORD input) const;

    // Function to check if the input key matches the alt key expected in the shortcut
    bool CheckAltKey(const DWORD input) const;

    // Function to check if the input key matches the shift key expected in the shortcut
    bool CheckShiftKey(const DWORD input) const;

    // Function to set a key in the shortcut based on the passed key code argument. Returns false if it is already set to the same value. This can be used to avoid UI refreshing
    bool SetKey(const DWORD input);

    // Function to reset the state of a shortcut key based on the passed key code argument
    void ResetKey(const DWORD input);

    // Function to return the string representation of the shortcut in virtual key codes appended in a string by ";" separator.
    winrt::hstring ToHstringVK() const;

    // Function to return a vector of key codes in the display order
    std::vector<DWORD> GetKeyCodes();

    // Function to set a shortcut from a vector of key codes
    void SetKeyCodes(const std::vector<int32_t>& keys);

    // Function to check if all the modifiers in the shortcut have been pressed down
    bool CheckModifiersKeyboardState(KeyboardManagerInput::InputInterface& ii) const;

    // Function to check if any keys are pressed down except those in the shortcut
    bool IsKeyboardStateClearExceptShortcut(KeyboardManagerInput::InputInterface& ii) const;

    // Function to get the number of modifiers that are common between the current shortcut and the shortcut in the argument
    int GetCommonModifiersCount(const Shortcut& input) const;
};

using KeyShortcutUnion = std::variant<DWORD, Shortcut>;
using RemapBufferItem = std::vector<KeyShortcutUnion>;
using RemapBufferRow = std::pair<RemapBufferItem, std::wstring>;
using RemapBuffer = std::vector<RemapBufferRow>;
