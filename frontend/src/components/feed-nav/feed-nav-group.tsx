import React, { ReactNode } from "react";
import {
  List,
  ListSubheader,
  ListItem,
  Typography,
  styled,
} from "@mui/material";

const StyledSubheader = styled(ListSubheader)(({ theme }) => ({
  backgroundColor: "transparent",
  color: theme.palette.secondary.dark,
}));

interface FeedNavGroupProps {
  name: string;
  children: ReactNode;
}

const FeedNavGroup = ({ name, children }: FeedNavGroupProps) => (
  <List
    sx={{ width: "100%" }}
    disablePadding={true}
    subheader={
      <StyledSubheader color="inherit">
        <Typography fontWeight="bold" variant="h6">
          {name}
        </Typography>
      </StyledSubheader>
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
