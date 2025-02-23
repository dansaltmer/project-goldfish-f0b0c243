import React from "react";
import { Avatar, CardHeader } from "@mui/material";

interface FeedNavFooterProps {
  name: string;
  subheader: string;
  avatar: string;
}

const FeedNavFooter = ({ name, subheader, avatar }: FeedNavFooterProps) => (
  <CardHeader
    sx={{ backgroundColor: "primary.dark", flex: "" }}
    title={name}
    subheader={<em>{subheader}</em>}
    avatar={<Avatar src={avatar}></Avatar>}
  />
);

export default FeedNavFooter;
