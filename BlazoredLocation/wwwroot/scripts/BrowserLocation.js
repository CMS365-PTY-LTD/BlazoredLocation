export async function getBrowserLocation()
{
    return new Promise((resolve, reject) => {
    if (!navigator.geolocation)
    {
        reject(JSON.stringify({ Code: UNKNOWN_ERROR, Message: "Geolocation is not supported by this browser." }));
    } else
    {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                resolve(position);
            },
            (error) => {
            var message = "";
            switch (error.code)
            {
                case error.PERMISSION_DENIED:
                    message = "User denied the request for Geolocation."

                        break;
                case error.POSITION_UNAVAILABLE:
                    message = "Location information is unavailable."

                        break;
                case error.TIMEOUT:
                    message = "The request to get user location timed out."

                        break;
                case error.UNKNOWN_ERROR:
                    message = "An unknown error occurred."

                        break;
            }
            reject(JSON.stringify({ Code: error.code, Message: message }));
    }
            );
        }
    });
}