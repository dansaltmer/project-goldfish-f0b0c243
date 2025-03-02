"use client";

import React, { useEffect } from "react";
import FeedNavigation from "@/components/feed-nav";
import FeedView from "@/components/feed-view";
import Login from "@/components/login";
import { useAuth } from "react-oidc-context";

const Home = ({}) => {
  const auth = useAuth();

  // Strip auth code exchange once finished
  useEffect(() => {
    if (auth.isAuthenticated) {
      window.history.replaceState(null, "", "/");
    }
  }, [auth.isAuthenticated]);

  if (!auth.isAuthenticated) {
    return <Login />;
  }

  if (auth.error) {
    return <div>Error... {JSON.stringify(auth.error)}</div>;
  }

  return (
    <div className="container">
      <FeedNavigation user={auth.user!} />
      <FeedView />
    </div>
  );
};

export default Home;
