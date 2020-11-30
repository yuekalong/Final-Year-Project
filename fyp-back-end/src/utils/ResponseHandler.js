/*
 * MODULE NAME: RESPONSEHANDLER
 * PROGRAMMER: YUE KA LONG
 * VERSION: 1.0 (16 MAY 2020)
 *
 * PURPOSE: THIS IS THE RESPONSE HANDLE OF THE BACK-END.
 *          IT HELPS TO HANDLE THE RESPONSE FORMAT OF EXPRESS.JS
 *
 *
 */

// this module is used to handle the res in a certain format
/*  {
      success: true
      data: result,
    }
*/

function responseHandler(responseInfo) {
  let response = { success: responseInfo.success };
  // check the respose is success or not
  if (responseInfo.error) {
    // not success, set the response is error
    response.error = responseInfo.error;
  }
  if (responseInfo.data !== undefined) {
    // if there have data in response, assign the data into the response object
    response.data = responseInfo.data;
  }

  // return the response
  return responseInfo.res.status(responseInfo.statusCode).json(response);
}

module.exports = {
  standardServiceResponse: function (res, next, data) {
    // set the reponse into specfic format
    return data
      .then((result) => {
        let responseInfo = {
          res: res,
          data: result,
          statusCode: 200,
          success: true,
        };
        return responseHandler(responseInfo);
      })
      .catch((err) => {
        // if the response have error return a error format
        next(
          new Error(
            JSON.stringify({ statusCode: 500, message: `${err.message}` })
          )
        );
      });
  },
};
