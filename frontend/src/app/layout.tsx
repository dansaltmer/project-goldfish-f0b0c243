import type { Metadata } from "next";
import { AppRouterCacheProvider } from "@mui/material-nextjs/v15-appRouter";
import { Geist, Geist_Mono } from "next/font/google";
import AuthProvider from "../providers/AuthProvider";
import ThemeProvider from "../providers/ThemeProvider";
import ConfigProvider from "../providers/ConfigProvider";
import "./globals.css";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "goldfish / ws poc / ds",
  description: "AWS WebSocket POC App",
};

interface RootLayoutProps {
  children: React.ReactNode;
}

export default function RootLayout({ children }: RootLayoutProps) {
  return (
    <html lang="en">
      <ThemeProvider>
        <body className={`${geistSans.variable} ${geistMono.variable}`}>
          <ConfigProvider>
            <AuthProvider>
              <AppRouterCacheProvider>{children}</AppRouterCacheProvider>
            </AuthProvider>
          </ConfigProvider>
        </body>
      </ThemeProvider>
    </html>
  );
}
