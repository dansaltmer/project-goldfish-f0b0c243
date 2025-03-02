"use client";

import { ReactNode } from "react";
import { createTheme, ThemeProvider } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    primary: {
      main: "#838579",
    },
    secondary: {
      main: "#6f7066",
    },
  },

  // try and replace with styled
  components: {
    MuiListItemButton: {
      styleOverrides: {
        root: ({ theme }) => ({
          "&:hover": {
            backgroundColor: theme.palette.secondary.main,
            color: theme.palette.primary.light,
          },
          "&.Mui-selected": {
            backgroundColor: theme.palette.secondary.main,
            color: theme.palette.primary.light,
          },
          "&.Mui-selected:hover": {
            backgroundColor: theme.palette.secondary.dark,
            color: theme.palette.primary.light,
          },
        }),
      },
    },
  },
});

interface GoldfishThemeProps {
  children: ReactNode;
}

const GoldfishThemeProvider = ({ children }: GoldfishThemeProps) => {
  return <ThemeProvider theme={theme}>{children}</ThemeProvider>;
};

export default GoldfishThemeProvider;
