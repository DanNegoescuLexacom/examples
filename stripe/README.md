# Development Notes

## Customer 

* We will need to create a Stripe customer if one does not exist prior to creating a SetupIntent.
* If customer can see their Stripe details, updates to Sardis customer records will need to be correlated w. Stripe (e.g. emails, etc.)
  * Might make sense for this to be done via RabbitMQ so we get repeatability.