import React from "react";
import { List, ListItem } from "@mui/material";
import FeedNavHeader from "./feed-nav-header";
import FeedNavGroup from "./feed-nav-group";
import FeedNavGroupItem from "./feed-nav-group-item";
import FeedNavFooter from "./feed-nav-footer";

interface FeedNavProps {
  user?: GoldfishAuthentication;
}

const FeedNav = ({ user }: FeedNavProps) => (
  <nav>
    <FeedNavHeader />
    <List disablePadding={true} style={{ flexGrow: "1" }}>
      <ListItem disableGutters={true}>
        <FeedNavGroup name="Intro">
          <FeedNavGroupItem selected name="welcome" />
          <FeedNavGroupItem name="other" />
          <FeedNavGroupItem name="example" />
        </FeedNavGroup>
      </ListItem>
    </List>
    {user && (
      <FeedNavFooter
        name={user.name}
        avatar={user.avatar}
        subheader={user.email}
      />
    )}
  </nav>
);

export default FeedNav;
