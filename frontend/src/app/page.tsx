"use client";

import React, { useState, useEffect } from "react";
import { useAuth } from "react-oidc-context";
import FeedNavigation from "@/components/feed-nav";
import FeedView from "@/components/feed-view";
import Login from "@/components/login";
import getFeedMessages from "@/api/feed/getFeedMessages";

// messy, abstract at some point

const Home = ({}) => {
  const auth = useAuth();

  // when moving to multi-channel, redux is probably better
  const [loading, setLoading] = useState(true);
  const [feed] = useState("welcome");
  const [items, setItems] = useState<FeedMessage[]>([]);

  // Strip auth code exchange once finished
  useEffect(() => {
    if (auth.isAuthenticated) {
      window.history.replaceState(null, "", "/");
    }
  }, [auth.isAuthenticated]);

  // If we're still loading and now have a token, then get from the api
  useEffect(() => {
    if (loading && auth.user?.access_token) {
      getFeedMessages(auth.user.access_token, feed).then((items) => {
        setItems(items);
        setLoading(false);
      });
    }
  }, [loading, auth.user?.access_token, feed]);

  if (!auth.isAuthenticated) {
    return <Login />;
  }

  if (auth.error) {
    return <div>Error... {JSON.stringify(auth.error)}</div>;
  }

  if (loading) {
    return <div>Replace with spinner.....</div>;
  }

  return (
    <div className="container">
      <FeedNavigation user={auth.user!} />
      <FeedView items={items} />
    </div>
  );
};

export default Home;
