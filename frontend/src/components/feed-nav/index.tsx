import React from "react";
import { List, ListItem } from "@mui/material";
import FeedNavHeader from "./feed-nav-header";
import FeedNavGroup from "./feed-nav-group";
import FeedNavGroupItem from "./feed-nav-group-item";
import FeedNavFooter from "./feed-nav-footer";
import { User } from "oidc-client-ts";

interface FeedNavProps {
  user: User;
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
    <FeedNavFooter
      name={user.profile.name ?? user.profile.email!}
      avatar={user.profile.picture}
      subheader={user.profile.email!}
    />
  </nav>
);

export default FeedNav;
