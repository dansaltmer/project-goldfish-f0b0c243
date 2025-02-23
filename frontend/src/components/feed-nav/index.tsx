import React from "react";
import { List, ListItem, Typography } from "@mui/material";
import FeedNavGroup from "./feed-nav-group";
import FeedNavGroupItem from "./feed-nav-group-item";

const FeedNav = ({}) => (
  <nav>
    <div className="nav-header">
      <Typography variant="h5" fontWeight="bold">
        project goldfish
      </Typography>
      <Typography color="secondary.dark" fontWeight="bold">
        ws poc / ds
      </Typography>
    </div>
    <List disablePadding={true}>
      <ListItem disableGutters={true}>
        <FeedNavGroup name="Intro">
          <FeedNavGroupItem selected name="welcome" />
          <FeedNavGroupItem name="other" />
          <FeedNavGroupItem name="example" />
        </FeedNavGroup>
      </ListItem>
    </List>
  </nav>
);

export default FeedNav;
