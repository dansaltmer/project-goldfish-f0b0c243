"use client";

import React, { useState } from "react";
import FeedNavigation from "@/components/feed-nav";
import FeedView from "@/components/feed-view";
import Authentication from "@/components/authentication";

const Home = ({}) => {
  const [user, setUser] = useState<GoldfishAuthentication | null>(null);

  if (!user) {
    return <Authentication onSuccess={(x) => setUser(x)} />;
  }

  return (
    <div className="container">
      <FeedNavigation user={user} />
      <FeedView />
    </div>
  );
};

export default Home;
