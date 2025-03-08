const getFeedMessages = (token: string, feed: string) => {
  const options = {
    headers: {
      authorization: `Bearer ${token}`,
    },
  };

  return fetch(`/api/${feed}/messages`, options)
    .then((res) => res.json())
    .then((obj) => obj as FeedMessage[]);
};

export default getFeedMessages;
