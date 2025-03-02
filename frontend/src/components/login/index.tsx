"use client";

import React from "react";
import { Button, Stack, styled } from "@mui/material";
import { useAuth } from "react-oidc-context";
import FeedNavHeader from "../feed-nav/feed-nav-header";

const Container = styled(Stack)({
  display: "flex",
  justifySelf: "center",
  marginTop: "300px",
});

const Login = () => {
  const auth = useAuth();
  return (
    <Container>
      <FeedNavHeader />
      <Button
        variant="contained"
        color="secondary"
        onClick={() => auth.signinRedirect()}
      >
        Sign In
      </Button>
    </Container>
  );
};

export default Login;
