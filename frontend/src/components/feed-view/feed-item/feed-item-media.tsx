import React from "react";
import { CardMedia } from "@mui/material";

interface FeedItemMediaProps {
  component: string;
  src: string;
  height?: string;
}

const FeedItemMedia = (props: FeedItemMediaProps) => (
  <CardMedia {...props} sx={{ border: "none" }} />
);

export default FeedItemMedia;
