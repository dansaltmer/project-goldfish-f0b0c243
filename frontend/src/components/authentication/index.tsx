import React from "react";
import { styled, Stack } from "@mui/material";
import { GoogleLogin, CredentialResponse } from "@react-oauth/google";
import FeedNavHeader from "../feed-nav/feed-nav-header";

const Container = styled(Stack)({
  display: "flex",
  justifySelf: "center",
  marginTop: "300px",
});

const parseUser = ({
  credential,
}: CredentialResponse): GoldfishAuthentication => {
  const claims = JSON.parse(atob(credential!.split(".")[1]));
  return {
    token: credential!,
    name: `${claims.given_name} ${claims.family_name[0]}`,
    email: claims.email,
    avatar: claims.picture,
    exp: claims.exp,
  };
};

interface AuthenticationProps {
  onSuccess: (auth: GoldfishAuthentication) => void;
}

const Authentication = ({ onSuccess }: AuthenticationProps) => {
  return (
    <Container>
      <FeedNavHeader />
      <GoogleLogin onSuccess={(cred) => onSuccess(parseUser(cred))} />
    </Container>
  );
};

export default Authentication;
