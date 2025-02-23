import React from "react";
import { styled, Chip, Stack } from "@mui/material";

const StyledStack = styled(Stack)({
  position: "absolute",
  bottom: "-15px",
  right: "0px",
});

const StyledChip = styled(Chip)(({}) => ({
  padding: "0",
  marginRight: "5px",
  borderRadius: "12px",
  fontSize: "14px",
}));

const FeedItemActions = ({}) => (
  <StyledStack direction="row">
    <StyledChip
      label={"✅1,534"}
      sx={{
        backgroundColor: "success.dark",
        color: "success.contrastText",
      }}
    />
    <StyledChip
      label={"❌34"}
      sx={{
        backgroundColor: "error.dark",
        color: "error.contrastText",
      }}
    />
  </StyledStack>
);

export default FeedItemActions;
