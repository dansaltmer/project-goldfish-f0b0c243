import React from "react";
import { Avatar, Stack, Typography } from "@mui/material";

interface FeedItemFooterProps {
  name: string;
  time: string;
  avatar?: string;
}

const FeedItemFooter = ({ name, time, avatar }: FeedItemFooterProps) => (
  <Stack direction="row" spacing={1} sx={{ padding: "2px 5px" }}>
    <Avatar src={avatar} sx={{ width: 24, height: 24 }}>
      {avatar ? "" : "?"}
    </Avatar>
    <Typography variant="body2" fontWeight="bold" sx={{ lineHeight: "24px" }}>
      {name}
    </Typography>
    <Typography
      variant="caption"
      color="secondary.light"
      sx={{ lineHeight: "24px" }}
    >
      {time}
    </Typography>
  </Stack>
);

export default FeedItemFooter;
