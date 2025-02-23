import React from "react";
import { ListItem, ListItemButton, Typography } from "@mui/material";

interface FeedNavGroupItemProps {
  name: string;
  selected?: boolean;
}

const FeedNavGroupItem = ({ name, selected }: FeedNavGroupItemProps) => (
  <ListItem disablePadding>
    <ListItemButton selected={selected}>
      #&nbsp;<Typography fontFamily="Consolas, serif">{name}</Typography>
    </ListItemButton>
  </ListItem>
);

export default FeedNavGroupItem;
