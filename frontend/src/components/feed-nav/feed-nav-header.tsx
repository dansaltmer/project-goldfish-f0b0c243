import React from "react";
import { Typography } from "@mui/material";

const FeedNavHeader = ({}) => (
  <div className="nav-header">
    <Typography variant="h5" fontWeight="bold">
      project goldfish
    </Typography>
    <Typography color="secondary.dark" fontWeight="bold">
      ws poc / ds
    </Typography>
  </div>
);

export default FeedNavHeader;
