// vuetify.config.ts
import { defineVuetifyConfiguration } from 'vuetify-nuxt-module/custom-configuration';

export default defineVuetifyConfiguration({
  defaults: {
    IconBtn: {
      icon: true,
      color: 'default',
      variant: 'text',
    },
    VAlert: {
      VBtn: {
        color: undefined,
      },
    },
    VAvatar: {
      variant: 'flat',
    },
    VCard: {
      elevation: 6,
      density: 'compact',
    },
    VBadge: {
      color: 'primary',
    },
    VBtn: {
      color: 'primary',
    },
    VChip: {
      elevation: 0,
    },
    VMenu: {
      offset: '2px',
    },
    VPagination: {
      density: 'compact',
      showFirstLastPage: true,
      variant: 'tonal',
    },
    VTabs: {
      color: 'primary',
      density: 'compact',
      VSlideGroup: {
        showArrows: true,
      },
    },
    VTooltip: {
      location: 'top',
    },
    VCheckboxBtn: {
      color: 'primary',
    },
    VCheckbox: {
      color: 'primary',
      density: 'comfortable',
      hideDetails: 'auto',
    },
    VRadioGroup: {
      color: 'primary',
      density: 'comfortable',
      hideDetails: 'auto',
    },
    VRadio: {
      density: 'comfortable',
      hideDetails: 'auto',
    },
    VSelect: {
      variant: 'solo',
      color: 'primary',
      hideDetails: 'auto',
      density: 'compact',
    },
    VRangeSlider: {
      color: 'primary',
      thumbLabel: true,
      hideDetails: 'auto',
      trackSize: 6,
      thumbSize: 22,
      elevation: 4,
    },
    VRating: {
      activeColor: 'warning',
      color: 'disabled',
    },
    VProgressCircular: {
      color: 'primary',
    },
    VProgressLinear: {
      color: 'primary',
    },
    VSlider: {
      color: 'primary',
      trackSize: 6,
      hideDetails: 'auto',
      thumbSize: 22,
      elevation: 4,
    },
    VSnackbar: {
      VBtn: {
        size: 'small',
      },
    },
    VTextField: {
      variant: 'solo',
      density: 'compact',
      color: 'primary',
      hideDetails: 'auto',
    },
    VAutocomplete: {
      variant: 'solo',
      color: 'primary',
      density: 'compact',
      hideDetails: 'auto',
    },
    VCombobox: {
      variant: 'solo',
      color: 'primary',
      hideDetails: 'auto',
      density: 'compact',
    },
    VFileInput: {
      variant: 'solo',
      color: 'primary',
      hideDetails: 'auto',
      density: 'compact',
    },
    VTextarea: {
      variant: 'solo',
      color: 'primary',
      hideDetails: 'auto',
      density: 'compact',
    },
    VSwitch: {
      inset: true,
      color: 'primary',
      hideDetails: 'auto',
    },
    VNavigationDrawer: {
      touchless: true,
    },
  },
  icons: {
    defaultSet: 'mdi',
  },
  theme: {
    defaultTheme: 'light',
    themes: {
      light: {
        dark: false,
        colors: {
          primary: '#FBBF24',
          secondary: '#2460FB',
          'on-secondary': '#fff',
          success: '#00CC9A',
          info: '#00D0FF',
          warning: '#FFB400',
          error: '#FF40B6',
          'on-primary': '#FFFFFF',
          'on-success': '#FFFFFF',
          'on-warning': '#FFFFFF',
          background: '#FFF8F0',
          'on-background': '#3A3541',
          'on-surface': '#3A3541',
          'grey-50': '#F9FAFB',
          'grey-100': '#F3F4F6',
          'grey-200': '#E5E7EB',
          'grey-300': '#D1D5DB',
          'grey-400': '#9CA3AF',
          'grey-500': '#6B7280',
          'grey-600': '#4B5563',
          'grey-700': '#374151',
          'grey-800': '#1F2937',
          'grey-900': '#111827',
        },
        variables: {
          'medium-emphasis-opacity': 0.68,
        },
      },
    },
  },
});
