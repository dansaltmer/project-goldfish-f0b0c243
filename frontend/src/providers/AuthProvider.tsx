"use client";

import { useContext, ReactNode } from "react";
import { AuthProvider } from "react-oidc-context";
import { ConfigContext } from "./ConfigProvider";
import { WebStorageStateStore } from "oidc-client-ts";

// Defaults to session store, switching this to local to persist longer
const userStore = new WebStorageStateStore({ store: window.localStorage });

interface CognitoAuthProviderProps {
  children: ReactNode;
}

const CognitoAuthProvider = ({ children }: CognitoAuthProviderProps) => {
  const config = useContext(ConfigContext);
  return (
    <AuthProvider {...config!.auth} userStore={userStore}>
      {children}
    </AuthProvider>
  );
};

export default CognitoAuthProvider;
