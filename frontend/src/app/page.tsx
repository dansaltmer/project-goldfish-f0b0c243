"use client";

import FeedNavigation from "@/components/feed-nav";
import FeedView from "@/components/feed-view";

export default function Home() {
  return (
    <div className="container">
      <FeedNavigation />
      <FeedView />
    </div>
  );
}
