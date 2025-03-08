export const handler = async (event, _) => {
  const attr = event.request.userAttributes;
  console.log("Attributes: ", JSON.stringify(attr, null, 2));

  const claims = {
    name: attr.name,
    email: attr.email,
    picture: attr.picture,
  };

  event.response = {
    claimsAndScopeOverrideDetails: {
      idTokenGeneration: {
        claimsToAddOrOverride: claims,
      },
      accessTokenGeneration: {
        claimsToAddOrOverride: claims,
      },
    },
  };

  return event;
};
