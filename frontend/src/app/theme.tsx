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

  components: {
    MuiListSubheader: {
      styleOverrides: {
        root: ({ theme }) => ({
          backgroundColor: "transparent",
          color: theme.palette.secondary.dark,
        }),
      },
    },
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

interface ChatAppThemeProps {
  children: ReactNode;
}

export default function ChatAppTheme({ children }: ChatAppThemeProps) {
  return <ThemeProvider theme={theme}>{children}</ThemeProvider>;
}
