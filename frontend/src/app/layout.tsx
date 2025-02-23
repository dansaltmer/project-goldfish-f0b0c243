import type { Metadata } from "next";
import { AppRouterCacheProvider } from "@mui/material-nextjs/v15-appRouter";
import { Geist, Geist_Mono } from "next/font/google";
import { GoogleOAuthProvider } from "@react-oauth/google";
import ThemeProvider from "./theme";
import "./globals.css";

// Could move to config, but its client side, so per build anyway.
const clientId =
  "1092886426735-66e2jfruka1r27tojcqlmfs3qbknh8c5.apps.googleusercontent.com";

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

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <ThemeProvider>
        <GoogleOAuthProvider clientId={clientId}>
          <body className={`${geistSans.variable} ${geistMono.variable}`}>
            <AppRouterCacheProvider>{children}</AppRouterCacheProvider>
          </body>
        </GoogleOAuthProvider>
      </ThemeProvider>
    </html>
  );
}
