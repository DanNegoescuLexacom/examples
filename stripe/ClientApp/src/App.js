import React, { useEffect, useState } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import Payments from './components/Payments';
import { Elements } from '@stripe/react-stripe-js';
import { loadStripe } from '@stripe/stripe-js';

import './custom.css'

const stripePromise = loadStripe('pk_test_51L1REtJNcUVNlmQAZ1DPXk2CtoTnzhCYweLaqWRRX52ZAOmZcDrdgjpKT7ymKG8HXU1gqvl9zbfllVddSfIHOHat005RWSEaDn');

function App() {
  const [clientSecret, setClientSecret] = useState(null);

  useEffect(() => {
    const getPaymentIntent = async () => {
      console.debug("getPaymentIntent");
      const response = await fetch('customers/1/setupintent');
      console.debug("response", response);
      var json = await response.json();
      console.debug("fetched client secret: ", json);
      setClientSecret(json.clientSecret);
    };

    console.debug("in useEffec");
    getPaymentIntent();
  }, []);

  console.debug('initializing app', clientSecret);

  const options = {
    clientSecret,
    // todo: customise look and feel of Stripe elems using Appearance API
    appearance: {}
  }

  console.debug("rendering app");

  return !clientSecret ? (<div>No secret</div>) : (
    // todo: I don't really understand this as the clientSecret is per-intent (setup or payment), 
    // so it's not like we can just do this call once during init. 
    // But Elements needs to wrap the entire app, and you cannot change the clientSecret after the first call.
    // Or is the clientSecret a per-session thing? 
    <Elements stripe={stripePromise} options={options}>
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/payments' component={Payments} />
        <Route path='/fetch-data' component={FetchData} />
      </Layout>
    </Elements>
  );
}

export default App;
