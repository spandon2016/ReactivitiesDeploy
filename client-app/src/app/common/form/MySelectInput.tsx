import React from 'react';
import { useField } from 'formik'; 
import { Form, Label, Select } from 'semantic-ui-react';

interface Props {
    placeholder: string;
    name: string;
    options: any;
    label?: string;

}

export default function MySelectInput(props: Props) {
    const [field, metal, helpers] = useField(props.name);
    return(
        <Form.Field error={metal.touched && !!metal.error}>
            <label>{props.label}</label>
            <Select 
            clearable
            options={props.options}
            value={field.value || null }
            onChange={(e, d) => helpers.setValue(d.value)}
            onBlue={() => helpers.setTouched(true)}
            placeholder={props.placeholder}
            />
            {metal.touched && metal.error ? (
                <Label basic color='red'>
                    {metal.error}

                </Label>
            ) : null}
        </Form.Field>
    )


}
