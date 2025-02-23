import React, { ReactNode } from "react";
import { List, ListSubheader, ListItem, Typography } from "@mui/material";

interface FeedNavGroupProps {
  name: string;
  children: ReactNode;
}

const FeedNavGroup = ({ name, children }: FeedNavGroupProps) => (
  <List
    sx={{ width: "100%" }}
    disablePadding={true}
    subheader={
      <ListSubheader color="inherit">
        <Typography fontWeight="bold" variant="h6">
          {name}
        </Typography>
      </ListSubheader>
    }
  >
    {children}
    <ListItem>
      <Typography variant="caption" color="secondary.dark">
        <em>Different feeds not yet implemented</em>
      </Typography>
    </ListItem>
  </List>
);

export default FeedNavGroup;
