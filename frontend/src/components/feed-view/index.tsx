import React from "react";
import FeedItem from "./feed-item";
import { Masonry } from "@mui/lab";

interface FeedViewProps {
  items: FeedMessage[];
}

const FeedView = ({ items }: FeedViewProps) => {
  return (
    <main>
      <Masonry columns={3} spacing={4} sx={{ margin: "2px" }}>
        {items.map((item) => (
          <FeedItem
            key={item.id}
            time={item.timestamp}
            name={item.profile.name}
            avatar={item.profile.avatar}
            media={item.media}
          >
            {item.text}
          </FeedItem>
        ))}
      </Masonry>
    </main>
  );
};

export default FeedView;
