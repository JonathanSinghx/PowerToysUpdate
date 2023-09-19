#pragma once

#include <FancyZonesLib/VirtualDesktop.h>
#include <FancyZonesLib/WindowUtils.h>

namespace FancyZonesWindowProcessing
{
    inline bool IsProcessable(HWND window) noexcept
    {
        const bool isSplashScreen = FancyZonesWindowUtils::IsSplashScreen(window);
        if (isSplashScreen)
        {
            return false;
        }

        const bool windowMinimized = IsIconic(window);
        if (windowMinimized)
        {
            return false;
        }

        // For windows that FancyZones shouldn't process (start menu, tray, popup menus) 
        // VirtualDesktopManager is unable to retrieve virtual desktop id and returns an error.
        auto desktopId = VirtualDesktop::instance().GetDesktopId(window);
        if (!desktopId.has_value())
        {
            return false;
        }

        // Switch between virtual desktops results with posting same windows messages that also indicate
        // creation of new window. We need to check if window being processed is on currently active desktop.
        if (!VirtualDesktop::instance().IsWindowOnCurrentDesktop(window))
        {
            return false;
        }

        return true;
    }
}