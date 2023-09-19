#pragma once

#if defined(UNIT_TESTS)
#include <common/SettingsAPI/settings_helpers.h>
#endif

#if defined(UNIT_TESTS)
namespace FancyZonesUnitTests
{
    class LayoutHotkeysUnitTests;
    class LayoutTemplatesUnitTests;
    class CustomLayoutsUnitTests;
    class AppliedLayoutsUnitTests;
}
#endif

class FancyZonesData
{
public:
    FancyZonesData();

    void ReplaceZoneSettingsFileFromOlderVersions();

    inline const std::wstring& GetSettingsFileName() const
    {
        return settingsFileName;
    }

private:
#if defined(UNIT_TESTS)
    friend class FancyZonesUnitTests::LayoutHotkeysUnitTests;
    friend class FancyZonesUnitTests::LayoutTemplatesUnitTests;
    friend class FancyZonesUnitTests::CustomLayoutsUnitTests;
    friend class FancyZonesUnitTests::AppliedLayoutsUnitTests;

    inline void SetSettingsModulePath(std::wstring_view moduleName)
    {
        std::wstring result = PTSettingsHelper::get_module_save_folder_location(moduleName);
        zonesSettingsFileName = result + L"\\" + std::wstring(L"zones-settings.json");
        appZoneHistoryFileName = result + L"\\" + std::wstring(L"app-zone-history.json");
    }

    inline std::wstring GetZoneSettingsPath(std::wstring_view moduleName)
    {
        std::wstring result = PTSettingsHelper::get_module_save_folder_location(moduleName);
        return result + L"\\" + std::wstring(L"zones-settings.json");
    }
#endif
    std::wstring settingsFileName;
    std::wstring zonesSettingsFileName;
    std::wstring appZoneHistoryFileName;
};

FancyZonesData& FancyZonesDataInstance();