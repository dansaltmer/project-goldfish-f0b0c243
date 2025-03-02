"use client";

import React, { createContext, useEffect, useState, ReactNode } from "react";
import { Stack, styled } from "@mui/material";

const clientConfigPath = "/config/client.json";

const Container = styled(Stack)({
  display: "flex",
  justifySelf: "center",
  marginTop: "300px",
});

interface GoldfishAuthConfig {
  authority: string;
  client_id: string;
  redirect_uri: string;
  response_type: string;
  scope: string;
}

interface GoldfishConfig {
  auth: GoldfishAuthConfig;
}

interface ConfigProviderProps {
  children: ReactNode;
}

export const ConfigContext = createContext<GoldfishConfig | null>(null);

const ConfigProvider = ({ children }: ConfigProviderProps) => {
  const [loading, setLoading] = useState(true);
  const [config, setConfig] = useState<GoldfishConfig | null>(null);

  useEffect(() => {
    setLoading(true);
    fetch(clientConfigPath)
      .then((res) => res.json())
      .then((obj) => obj as GoldfishConfig)
      .then((cfg) => {
        setConfig(cfg);
        setLoading(false);
      })
      .catch(console.error);
  }, []);

  if (loading) {
    return <Container>Replace with a spinner....</Container>;
  }

  return (
    <ConfigContext.Provider value={config}>{children}</ConfigContext.Provider>
  );
};

export default ConfigProvider;
