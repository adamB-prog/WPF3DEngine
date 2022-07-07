#version 330 core

layout(location = 0) out vec4 color;


in vec2 v_TexCoord;
in vec3 Normal;
in vec3 FragPos;

uniform float v_Alpha = 0.7;
uniform sampler2D u_Texture;
uniform vec3 lightPos;
uniform vec3 viewPos;


vec3 lightColor = vec3(1,0.5,1);
float ambientStrength = 0.01;
float specularStrength = 0.1;
vec3 ambient = ambientStrength * lightColor;


void main()
{
	vec3 norm = Normal;
	vec3 lightDir = normalize(lightPos - FragPos);

	vec3 viewDir = normalize(viewPos - FragPos);
	vec3 reflectDir = reflect(lightDir, norm);

	float diff = max(dot(norm, lightDir), 0.0);
	vec3 diffuse = diff * lightColor;
	
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
	vec3 specular = specularStrength * spec * lightColor;


	vec4 texColor = texture(u_Texture, v_TexCoord);
	vec3 part = vec3(texColor.x, texColor.y, texColor.z) * (ambient + diffuse + specular);
	color = vec4(part,1.0);
}