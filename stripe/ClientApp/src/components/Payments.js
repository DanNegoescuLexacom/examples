import React, { useState } from 'react';
import {useStripe, useElements, PaymentElement} from '@stripe/react-stripe-js';

const Payments = () => {
  console.debug("Payments");
  const stripe = useStripe();
  const elements = useElements();

  const [errorMessage, setErrorMessage] = useState(null);

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!stripe || !elements) {
      console.debug('Stripe or Elements not loaded.');
      // Stripe.js has not yet loaded.
      // Make sure to disable form submission until Stripe.js has loaded.
      return;
    }

    const {error} = await stripe.confirmSetup({
      //`Elements` instance that was used to create the Payment Element
      elements,
      confirmParams: {
        // todo: this is the location the page is redirected to on success, so needs to be a bit more logical.
        return_url: window.location.toString(),

      }
    });

    if (error) {
      // This point will only be reached if there is an immediate error when
      // confirming the payment. Show error to your customer (for example, payment
      // details incomplete)
      setErrorMessage(error.message);
    } else {
      // Your customer will be redirected to your `return_url`. For some payment
      // methods like iDEAL, your customer will be redirected to an intermediate
      // site first to authorize the payment, then redirected to the `return_url`.
    }
  }

  console.debug("Rendering Payments");

  return (
    <div>
      <h1>Payments</h1>
      <form onSubmit={handleSubmit}>
        <PaymentElement />
        <button disabled={!stripe}>Submit</button>
        {errorMessage && <div>{errorMessage}</div>}
      </form>
    </div>
  );
}

export default Payments;