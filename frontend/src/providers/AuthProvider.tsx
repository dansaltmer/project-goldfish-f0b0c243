"use client";

import { useContext, ReactNode } from "react";
import { AuthProvider } from "react-oidc-context";
import { ConfigContext } from "./ConfigProvider";

interface CognitoAuthProviderProps {
  children: ReactNode;
}

const CognitoAuthProvider = ({ children }: CognitoAuthProviderProps) => {
  const config = useContext(ConfigContext);
  return <AuthProvider {...config!.auth}>{children}</AuthProvider>;
};

export default CognitoAuthProvider;
