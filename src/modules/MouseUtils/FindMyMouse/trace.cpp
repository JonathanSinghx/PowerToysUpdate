#include "pch.h"
#include "trace.h"

TRACELOGGING_DEFINE_PROVIDER(
    g_hProvider,
    "Microsoft.PowerToys",
    // {38e8889b-9731-53f5-e901-e8a7c1753074}
    (0x38e8889b, 0x9731, 0x53f5, 0xe9, 0x01, 0xe8, 0xa7, 0xc1, 0x75, 0x30, 0x74),
    TraceLoggingOptionProjectTelemetry());

void Trace::RegisterProvider() noexcept
{
    TraceLoggingRegister(g_hProvider);
}

void Trace::UnregisterProvider() noexcept
{
    TraceLoggingUnregister(g_hProvider);
}

// Log if the user has FindMyMouse enabled or disabled
void Trace::EnableFindMyMouse(const bool enabled) noexcept
{
    TraceLoggingWrite(
        g_hProvider,
        "FindMyMouse_EnableFindMyMouse",
        ProjectTelemetryPrivacyDataTag(ProjectTelemetryTag_ProductAndServicePerformance),
        TraceLoggingKeyword(PROJECT_KEYWORD_MEASURE),
        TraceLoggingBoolean(enabled, "Enabled"));
}

// Log that the user activated the module by focusing the mouse pointer
void Trace::MousePointerFocused() noexcept
{
    TraceLoggingWrite(
        g_hProvider,
        "FindMyMouse_MousePointerFocused",
        ProjectTelemetryPrivacyDataTag(ProjectTelemetryTag_ProductAndServicePerformance),
        TraceLoggingKeyword(PROJECT_KEYWORD_MEASURE));
}
