﻿#pragma once
#include "GPOWrapper.g.h"
#include <common/utils/gpo.h>

namespace winrt::PowerToys::GPOWrapper::implementation
{
    struct GPOWrapper : GPOWrapperT<GPOWrapper>
    {
        GPOWrapper() = default;
        static GpoRuleConfigured GetConfiguredAlwaysOnTopEnabledValue();
        static GpoRuleConfigured GetConfiguredAwakeEnabledValue();
        static GpoRuleConfigured GetConfiguredColorPickerEnabledValue();
        static GpoRuleConfigured GetConfiguredCropAndLockEnabledValue();
        static GpoRuleConfigured GetConfiguredFancyZonesEnabledValue();
        static GpoRuleConfigured GetConfiguredFileLocksmithEnabledValue();
        static GpoRuleConfigured GetConfiguredSvgPreviewEnabledValue();
        static GpoRuleConfigured GetConfiguredMarkdownPreviewEnabledValue();
        static GpoRuleConfigured GetConfiguredMonacoPreviewEnabledValue();
        static GpoRuleConfigured GetConfiguredMouseWithoutBordersEnabledValue();
        static GpoRuleConfigured GetConfiguredPdfPreviewEnabledValue();
        static GpoRuleConfigured GetConfiguredGcodePreviewEnabledValue();
        static GpoRuleConfigured GetConfiguredSvgThumbnailsEnabledValue();
        static GpoRuleConfigured GetConfiguredPdfThumbnailsEnabledValue();
        static GpoRuleConfigured GetConfiguredGcodeThumbnailsEnabledValue();
        static GpoRuleConfigured GetConfiguredStlThumbnailsEnabledValue();
        static GpoRuleConfigured GetConfiguredHostsFileEditorEnabledValue();
        static GpoRuleConfigured GetConfiguredImageResizerEnabledValue();
        static GpoRuleConfigured GetConfiguredKeyboardManagerEnabledValue();
        static GpoRuleConfigured GetConfiguredFindMyMouseEnabledValue();
        static GpoRuleConfigured GetConfiguredMouseHighlighterEnabledValue();
        static GpoRuleConfigured GetConfiguredMouseJumpEnabledValue();
        static GpoRuleConfigured GetConfiguredMousePointerCrosshairsEnabledValue();
        static GpoRuleConfigured GetConfiguredPowerRenameEnabledValue();
        static GpoRuleConfigured GetConfiguredPowerLauncherEnabledValue();
        static GpoRuleConfigured GetConfiguredQuickAccentEnabledValue();
        static GpoRuleConfigured GetConfiguredRegistryPreviewEnabledValue();
        static GpoRuleConfigured GetConfiguredScreenRulerEnabledValue();
        static GpoRuleConfigured GetConfiguredShortcutGuideEnabledValue();
        static GpoRuleConfigured GetConfiguredTextExtractorEnabledValue();
        static GpoRuleConfigured GetConfiguredPastePlainEnabledValue();
        static GpoRuleConfigured GetConfiguredVideoConferenceMuteEnabledValue();
        static GpoRuleConfigured GetConfiguredPeekEnabledValue();
        static GpoRuleConfigured GetDisableAutomaticUpdateDownloadValue();
        static GpoRuleConfigured GetAllowExperimentationValue();
    };
}

namespace winrt::PowerToys::GPOWrapper::factory_implementation
{
    struct GPOWrapper : GPOWrapperT<GPOWrapper, implementation::GPOWrapper>
    {
    };
}
